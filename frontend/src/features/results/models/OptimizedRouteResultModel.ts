import type RouteStopResultModel from "./RouteStopResultModel";

export default interface OptimizedRouteResultModel {
    totalDistance: number;
    stops: RouteStopResultModel[];
}