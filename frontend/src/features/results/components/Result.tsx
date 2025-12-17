import { useQuery } from "@tanstack/react-query";
import apiClient from "../../../api/axiosClient";
import Map from "../../../components/map/Map";
import type OptimizedRouteResultModel from "../models/OptimizedRouteResultModel";

export default function Result() {
    const { data, isLoading, isError, error } = useQuery<OptimizedRouteResultModel>({
        queryKey: ["optimized-route"],
        queryFn: async () => {
            const res = await apiClient.get<OptimizedRouteResultModel>("/routes");
            return res.data;
        },
    });

    if (isLoading) {
        return <p>Calculating optimal route...</p>;
    }

    if (isError) {
        return <p className="text-danger">{(error as Error).message}</p>;
    }

    if (!data || data.stops.length === 0) {
        return <p>No route available.</p>;
    }

    const coordinates = data.stops.map((stop, index) => ({
        lat: stop.latitude,
        lng: stop.longitude,
        message: `${index + 1}. ${stop.name}`,
    }));

    return (
        <div className="space-y-4">
            <h2 className="h4">Optimized Route</h2>

            <p>
                <strong>Total distance:</strong>{" "}
                {data.totalDistance.toFixed(2)} km
            </p>

            <Map coordinates={coordinates} />
        </div>
    );
}
