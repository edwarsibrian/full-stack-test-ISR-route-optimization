import CsvFileSelect from "../../../components/CsvFileSelect";
import apiClient from "../../../api/axiosClient";
/*import { useState } from "react";*/
import { useMutation } from "@tanstack/react-query";
import { useForm } from "react-hook-form";
import type { SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";

import Button from "../../../components/Button";
import type UploadLeadResultModel from "../models/UploadLeadResultModel";
/*import type UploadLeadsFormModel from "../models/UploadLeadsFormModel";*/
/*import type UploadLeadsInputModel from "../models/UploadLeadsInputModel";*/


//async function uploadLeadsFile(file: File, source: LeadSource): Promise<UploadLeadResultModel> {
//    const formData = new FormData();
//    formData.append("file", file);
//    formData.append("source", source);

//    const response = await apiClient.post<UploadLeadResultModel>("/leads/upload", formData);
//    return response.data;
//}

export default function UploadLeads(props: UploadLeadsProps) {

    //const initialFormState: UploadLeadsFormModel = {
    //    manager: { status: "idle" },
    //    isr: { status: "idle" }
    //};
    type UploadLeadsFormSchema = yup.InferType<typeof validationSchema>;
    type UploadLeadsFormModel = UploadLeadsFormSchema;

    const { handleSubmit, setValue, formState: { errors, isSubmitting } } = useForm<UploadLeadsFormSchema>({
        resolver: yupResolver(validationSchema),
    });

    const uploadMutation = useMutation({
        mutationFn: async (form: UploadLeadsFormModel) => {
            const formData = new FormData();

            if (form.manager.file) {
                formData.append("managerFile", form.manager.file);
            }

            if (form.isr.file) {
                formData.append("isrFile", form.isr.file);
            }

            const response = await apiClient.post<UploadLeadResultModel>("/leads/upload",formData);

            return response.data;
        },
        onSuccess: (data) => {
            props.onSuccess?.(data);
        },
        onError: (error) => {
            props.onError?.(error as Error);
        },
    });


    const onSubmit: SubmitHandler<UploadLeadsFormSchema> = (data) => {
        uploadMutation.mutate(data);
    };
            

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <h2>Upload Leads</h2>

            <CsvFileSelect
                label="Manager Lead List (CSV)"
                onFileSelect={(file) =>
                    setValue("manager.file", file, { shouldValidate: true })
                }
            />
            {errors.manager?.file && (
                <p className="text-danger">{errors.manager.file.message}</p>
            )}

            <CsvFileSelect
                label="Your Lead List (Optional CSV)"
                onFileSelect={(file) =>
                    setValue("isr.file", file, { shouldValidate: true })
                }
            />
            {errors.isr?.file && (
                <p className="text-danger">{errors.isr.file.message}</p>
            )}

            {uploadMutation.isError && (
                <p className="text-danger">
                    {(uploadMutation.error as Error).message}
                </p>
            )}

            <Button type="submit" disabled={isSubmitting || uploadMutation.isPending}>
                {uploadMutation.isPending ? "Uploading..." : "Upload Leads"}
            </Button>
        </form>
    );
}

//function UploadStatusView({ input }: { input: UploadLeadsInputModel }) {
//    switch (input.status) {
//        case "uploading":
//            return <p className="text-sm">Uploading...</p>;
//        case "success":
//            return (
//                <p className="text-sm text-green-600">
//                    {input.result?.importedLeads} leads imported
//                </p>
//            );
//        case "error":
//            return <p className="text-sm text-red-600">{input.error}</p>;
//        default:
//            return <p className="text-sm text-muted-foreground">No file uploaded</p>;
//    }
//}

interface UploadLeadsProps {
    onSuccess?: (result: UploadLeadResultModel) => void;
    onError?: (error: Error) => void;
}

const validationSchema = yup.object({
    manager: yup.object({
        file: yup
            .mixed<File>()
            .required("Manager CSV is required"),
    }),
    isr: yup.object({
        file: yup
            .mixed<File>()
            .nullable(),
    }),
});