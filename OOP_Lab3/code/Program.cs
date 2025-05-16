using OOP_LAB3.Presentation;
using OOP_LAB3.Presentation.UI;


var lst = DependencyInjection.Register();
var ui = new UI(lst);
await ui.RunAsync();