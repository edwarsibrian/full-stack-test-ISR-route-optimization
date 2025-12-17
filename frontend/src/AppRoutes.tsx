import { Route, Routes } from "react-router-dom";
import NotFoundRoute from "./components/NotFoundFoute";
import UploadLeads from "./features/upload-leads/components/UploadLeads";
import HomeAddress from "./features/home-address/components/HomeAddress";
import Result from "./features/results/components/Result";
import Home from "./features/home/components/Home";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Home />} />

            <Route path="/home-address" element={<HomeAddress />} />

            <Route path="/upload-leads" element={<UploadLeads />} />

            <Route path="/results" element={<Result />} />

            <Route path="*" element={<NotFoundRoute />} />'
        </Routes>
    )
}