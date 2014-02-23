namespace AzureTest.WebApp
open System.Web.Mvc

type HomeController() = 
    inherit Controller()
    member this.Index() = 
        this.View()
    member this.About() =
        this.View()
    member this.Contact() =
        this.View()