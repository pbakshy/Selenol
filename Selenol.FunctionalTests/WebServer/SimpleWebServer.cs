// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

using Selenol.Extensions;

namespace Selenol.FunctionalTests.WebServer
{
    public class SimpleWebServer : IDisposable
    {
        private readonly HttpListener listener;

        private readonly Assembly executingAssembly;

        private readonly string[] resourceNames;

        public SimpleWebServer(int port)
        {
            this.listener = new HttpListener();
            var prefix = "http://localhost:{0}/".FInv(port);
            this.listener.Prefixes.Add(prefix);
            this.listener.Start();

            this.executingAssembly = Assembly.GetExecutingAssembly();
            this.resourceNames = this.executingAssembly.GetManifestResourceNames();

            Task.Factory.StartNew(this.ServerLoop).ContinueWith(t => HandleException(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
        }

        public void Dispose()
        {
            this.listener.Stop();
            this.listener.Close();
        }

        private static string GetFilename(Uri url)
        {
            var filename = url.PathAndQuery.TrimStart('/');
            var questionMarkIndex = filename.IndexOf('?');
            if (questionMarkIndex >= 0)
            {
                filename = filename.Substring(0, questionMarkIndex);
            }

            return filename;
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            Trace.TraceError("Caught exception of type '{0}'. Message: {1}", ex.GetType(), ex.Message);
        }

        private void ServerLoop()
        {
            while (this.listener.IsListening)
            {
                var context = this.listener.GetContext();
                var filename = GetFilename(context.Request.Url);

                var name = this.resourceNames.FirstOrDefault(x => x.EndsWith("." + filename));
                if (name == null)
                {
                    context.Response.OutputStream.Close();
                    return;
                }

                using (var stream = this.executingAssembly.GetManifestResourceStream(name))
                {
                    if (stream != null)
                    {
                        stream.CopyTo(context.Response.OutputStream);
                    }
                }

                context.Response.OutputStream.Close();
            }
        }
    }
}