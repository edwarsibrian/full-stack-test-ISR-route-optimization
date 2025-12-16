import type { UploadStatus } from "../types/leads.types";
import type UploadLeadResultModel from "./UploadLeadResultModel";

export default interface UploadLeadsInputModel {
    file?: File;
    status: UploadStatus;
    result?: UploadLeadResultModel;
    error?: string;
}
