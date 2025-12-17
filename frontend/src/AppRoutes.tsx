import { Route, Routes } from "react-router-dom";
import NotFoundRoute from "./components/NotFoundFoute";
import UploadLeads from "./features/upload-leads/components/UploadLeads";
import HomeAddress from "./features/home-address/components/HomeAddress";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/" />

            <Route path="/home-address" element={<HomeAddress />} />

            <Route path="/upload-leads" element={<UploadLeads />} />

            <Route path="*" element={<NotFoundRoute />} />'
        </Routes>
    )
}