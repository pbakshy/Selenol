Selenol - Page objects for Selenium Web Driver
==============================================
Selenol is .NET implementation of [page object pattern](http://martinfowler.com/bliki/PageObject.html) based on Selenium Web Driver and written in C#.
Selenol allows you to write web tests easily and quickly. It heavy propagandizes page object pattern and has a lot of convenient helpers for this. 

#Installation
##Using Nuget
```
Install-Package Selenol
```
##Building from source
Just clone the repository and reference it to your existing project.

#Getting started
First of all we need to define your page. Lets say it will be Github home page.
```
using Selenol.Elements;
using Selenol.Page;
using Selenol.SelectorAttributes;
using Selenol.Validation.Page;

namespace Selenol.Sample
{
    [Url("/")]
    public class MainPage : BasePage
    {
        [Id("js-command-bar-field")]
        public virtual TextboxElement SearchBox { get; set; }
    }
}
```
All pages have to inherit BasePage and have an validation attribute. In our case it's Url which will check that page locates in the root of the site.
Page can contains web elements and controls. Controls are reusable sets of web elements. You can read about controls on [wiki](...)
Web elements have to be marked with a selector attribute. There are bunch of them Id, Class, Name, Tag, Css, XPath. 

Now you can create a browser object and open the page.
```
var firefox = new FirefoxDriver();
var mainPage = firefox.GoTo<MainPage>("https://github.com");
```
And search repositories using top search box.
```
mainPage.SearchBox.TypeText("Seleno");
```
Next need to press Enter and navigates to next page.
```
var searchPage = mainPage.Go(x => x.SearchBox.SendEnter()).To<SearchPage>();
```

You can find out more in the complete example that is in Selenol.Example project.