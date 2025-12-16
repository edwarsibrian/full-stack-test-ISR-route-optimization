import type { LeadSource } from "../types/leads.types";

export default interface UploadLeadResultModel {
    totalRows: number;
    importedLeads: number;
    failedLeads: number;
    source: LeadSource;
}