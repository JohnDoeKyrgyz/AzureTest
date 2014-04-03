namespace AzureTest.WebApp
open System
open System.Collections.Generic
open System.Web.Mvc
open Snotel

type SnotelStations = {
    State: string
    Stations: IEnumerable<string>}

type HomeController() = 
    inherit Controller()
    member private this.View(model : obj) = base.View(model)
    member this.Index(state) = 
        let state = if String.IsNullOrWhiteSpace(state) then "MT" else state
        async{            
            let! stations = GetStationsByState state
            let result = {State = state; Stations = stations}
            return this.View result }
        |> Async.StartAsTask
    member this.About() =
        this.View()
    member this.Contact() =
        this.View()