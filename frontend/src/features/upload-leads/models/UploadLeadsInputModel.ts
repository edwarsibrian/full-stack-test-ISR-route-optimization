export default interface UploadLeadsInputModel {
    file?: File;
    status: "idle" | "uploading" | "success" | "error";
