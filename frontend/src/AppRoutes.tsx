import { Route, Routes } from "react-router";
import NotFoundRoute from "./components/NotFoundFoute";
import UploadLeads from "./features/upload-leads/components/UploadLeads";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/" />

            <Route path="/upload-leads" element={<UploadLeads />} />

            <Route path='*' element={<NotFoundRoute />} />'
        </Routes>
    )
}