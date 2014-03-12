namespace AzureTest.WebApp

open System
open System.Web
open System.Web.Mvc
open System.Web.Routing
///All configuration information
module Configuration =

    type Route = { 
        controller : string
        action : string
        id : UrlParameter }

    type WebApiDefaults = {
        id : UrlParameter
    }

    /// Sets up routing for the application
    let configureRoutes (routes : RouteCollection) =
            
        //basic routeing for website
        let baseRoute = { controller = "Home"; action = "Index"; id = UrlParameter.Optional }
        
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
        routes.MapRoute("Default", "{controller}/{action}/{id}", baseRoute)
        
open Configuration

type Global() =
    inherit System.Web.HttpApplication()

    member this.Start() =
        
        AreaRegistration.RegisterAllAreas()

        configureRoutes( RouteTable.Routes )
        |> ignore