namespace AzureTest.WebApp

module Snotel =
    open Microsoft.FSharp.Data.TypeProviders
    open System
    open System.ServiceModel
    type SnowtelService = WsdlService<"http://www.wcc.nrcs.usda.gov/awdbWebService/services?WSDL">
    
    let service = SnowtelService.GetAwdbWebServiceImplPort()
    let binding = service.DataContext.Endpoint.Binding :?> BasicHttpBinding
    do
        binding.MaxReceivedMessageSize <- int64 Int32.MaxValue

    let GetStationsByState state =
        async {
            let! result = service.getStationsAsync(null, [|state|], [|"SNOW"|], null, null, 0m, 0m, 0m, 0m, 0m, 0m, null, null, null, false) |> Async.AwaitTask
            return result.``return``}

    let GetForecast station startDate endDate =
        async {
            let! response = service.getAllForecastsForStationAsync(station, startDate, endDate) |> Async.AwaitTask
            let forecasts = response.``return``
            let result = [
                for forecast in forecasts do
                    yield forecast.elementCd, forecast.coordinatedForecastValues]
            return result}
