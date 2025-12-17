export default interface RouteStopResultModel{
    leadId: string;
    name: string;
    address: string;
    latitude: number;
    longitude: number;
    distanceFromPrevious: number;
}