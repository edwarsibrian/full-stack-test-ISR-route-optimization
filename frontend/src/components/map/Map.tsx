import { MapContainer, Marker, TileLayer, useMapEvent, Popup } from "react-leaflet";
import type CoordinateModel from "./Coordinate.model";
import { useState } from "react";
//import { useMap } from "react-leaflet";
//import { useEffect } from "react";

export default function Map(props: MapProps) {

    const [coordinates, setCoordinates] = useState<CoordinateModel[] | undefined>(props.coordinates);
    return (
        <MapContainer
            center={[13.7013, -89.2244]}
            zoom={14} scrollWheelZoom={true} style={{ height: '500px' }} >
            <TileLayer attribution='ISR' url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />

            <MapClick setPoint={coordinate => {
                setCoordinates([coordinate]);
                if (props.selectedPlace) {
                    props.selectedPlace(coordinate);
                }
            }} />
            {/*<ResizeMap />*/}
            {coordinates?.map(coordinate =>
                <Marker key={coordinate.lat + coordinate.lng} position={[coordinate.lat, coordinate.lng]}>
                    {coordinate.message ? <Popup>{coordinate.message}</Popup> : undefined}
                </Marker>
            )}
        </MapContainer>
    )
}

function MapClick(props: MapClickProps) {

    useMapEvent('click', e => {
        props.setPoint({ lat: e.latlng.lat, lng: e.latlng.lng })
    })

    return null;
}

//function ResizeMap() {
//    const map = useMap();

//    useEffect(() => {
//        setTimeout(() => {
//            map.invalidateSize();
//        }, 0);
//    }, [map]);

//    return null;
//}

interface MapClickProps {
    setPoint: (coordinate: CoordinateModel) => void;
}

interface MapProps {
    selectedPlace?: (coordinate: CoordinateModel) => void;
    coordinates?: CoordinateModel[];
}