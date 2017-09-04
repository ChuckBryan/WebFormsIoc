# ASP.NET WebForms IoC Integration

This is a proof of concept based on [Jeffrey Fritz's MSDN Article](https://blogs.msdn.microsoft.com/webdev/2016/10/19/modern-asp-net-web-forms-development-dependency-injection/)

My project uses StructureMap instead of AutoFac and I extended it to also inject dependencies into UserControls that might be 
on the page.

| Class | Description|
|-------|------------|
|IoC.cs|Provides a static access to StructureMap's IContainer. This can be used in scenarios where you need to resort to Service Locator. The Static method calls the **ScanningRegistry**|
|ScanningRegistry|Typical StructureMap Scanner Registry to load all default configurations in the calling assembly and any additioanal Registries|
|IoCHttpModule|HttpModule that will attempt to inject dependencies into Pages and UserControls|
|IocExtensions|Helper Extensions that uses reflection to push Registered Services into the constructors of Web Pages and User Controls|

# Meat and Potatoes #
Please read [Jeffrey Fritz's MSDN Article](https://blogs.msdn.microsoft.com/webdev/2016/10/19/modern-asp-net-web-forms-development-dependency-injection/) article.
Also read through the comments. There is one really good one that references [this Gist](https://gist.github.com/adrianiftode/bec4c3c4a9e1e18160b122cade7382a2) that points out
an issue with a Page's Base Class's Constructor.

The main issue I had was that UserControls were not getting dependencies passed in. That was primarily because the UserControls are null in the
*PreRequestHandlerExecute* event. To overcome that, I added the method **IsPageRequest** and then wired up the InitComplete. 

# Default Constructors #
Because we can't replace the actual Page/UserControl builder factories, ASPX requires that both of these types have a default constructor.
Technically, these objects *are* created using the default constructor. The Module simply uses some reflection magic to call *another* constructor
after the Page/UserControl has been created and pass in the services.


