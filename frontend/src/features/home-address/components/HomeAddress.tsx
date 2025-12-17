import { useEffect, useState } from "react";
import Map from "../../../components/map/Map";
import type CoordinateModel from "../../../components/map/Coordinate.model";
import Button from "../../../components/Button";
import type HomeAddressApiModel from "../models/HomeAddressApiModel ";
import apiClient from "../../../api/axiosClient";
import type { AxiosError } from "axios";

export default function HomeAddress() {

    const [homeLocation, setHomeLocation] =
        useState<CoordinateModel | null>(null);

    const [isLoading, setIsLoading] = useState(true);
    const [isSaving, setIsSaving] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState(false);

    //Get
    useEffect(() => {
        const loadHomeAddress = async () => {
            try {
                const response = await apiClient.get<HomeAddressApiModel>(
                    "/home-address"
                );

                if (response.data) {
                    setHomeLocation({
                        lat: response.data.latitude,
                        lng: response.data.longitude,
                        message: "Your Home",
                    });
                }
            } catch (err) {
                // 204 No Content 
                const axiosError = err as AxiosError;
                if (axiosError.response?.status !== 204) {
                    setError("Failed to load home address");
                }
            } finally {
                setIsLoading(false);
            }
        };

        loadHomeAddress();
    }, []);

    //Post
    const handleSave = async () => {
        if (!homeLocation) return;

        setIsSaving(true);
        setError(null);
        setSuccess(false);

        try {
            await apiClient.post("/home-address", {
                latitude: homeLocation.lat,
                longitude: homeLocation.lng,
            });

            setSuccess(true);
        } catch {
            setError("Failed to save home address");
        } finally {
            setIsSaving(false);
        }
    };

    if (isLoading) {
        return <p>Loading home address...</p>;
    }


    return (
        <div className="space-y-4">
            <h2 className="h4">Home Address</h2>

            <p className="text-muted">
                Select your starting location by clicking on the map.
            </p>

            <Map
                coordinates={
                    homeLocation
                        ? [{ ...homeLocation, message: "Your Home" }]
                        : undefined
                }
                selectedPlace={(coordinate) =>
                    setHomeLocation({
                        ...coordinate,
                        message: "Your Home",
                    })
                }
            />

            {homeLocation && (
                <div className="alert alert-info mt-3">
                    <strong>Selected location:</strong>
                    <br />
                    Latitude: {homeLocation.lat.toFixed(6)}
                    <br />
                    Longitude: {homeLocation.lng.toFixed(6)}
                </div>
            )}

            {error && (
                <div className="alert alert-danger">
                    {error}
                </div>
            )}

            {success && (
                <div className="alert alert-success">
                    Home address saved successfully
                </div>
            )}

            <Button
                type="button"
                className="btn btn-success"
                disabled={!homeLocation || isSaving}
                onClick={handleSave}
            >
                {isSaving ? "Saving..." : "Save Home Address"}
            </Button>
        </div>
    );
}

//interface HomeAddressProps {
//    initialLocation?: CoordinateModel;
//    onSave?: (coordinate: CoordinateModel) => void;
//}
