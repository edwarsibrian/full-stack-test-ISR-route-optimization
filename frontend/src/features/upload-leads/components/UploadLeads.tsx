import CsvFileSelect from "../../../components/CsvFileSelect";
import apiClient from "../../../api/axiosClient";
import { useMutation } from "@tanstack/react-query";
import { useForm } from "react-hook-form";
import { useState } from "react";
import type { SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";

import Button from "../../../components/Button";
import type UploadLeadResultModel from "../models/UploadLeadResultModel";


export default function UploadLeads(props: UploadLeadsProps) {

    
    type UploadLeadsFormSchema = yup.InferType<typeof validationSchema>;
    type UploadLeadsFormModel = UploadLeadsFormSchema;

    const { handleSubmit, setValue, formState: { errors, isSubmitting } } = useForm<UploadLeadsFormSchema>({
        resolver: yupResolver(validationSchema),
    });

    const [result, setResult] = useState<UploadLeadResultModel | null>(null);

    const uploadMutation = useMutation({
        mutationFn: async (form: UploadLeadsFormModel) => {
            const formData = new FormData();

            if (form.manager.file) {
                formData.append("managerFile", form.manager.file);
            }

            if (form.isr.file) {
                formData.append("isrFile", form.isr.file);
            }

            const response = await apiClient.post<UploadLeadResultModel>(
                "/leads/upload",
                formData,
                {
                    headers: {
                        "Content-Type": "multipart/form-data",
                    },
                });

            return response.data;
        },
        onSuccess: (data) => {
            setResult(data);
            props.onSuccess?.(data);
        },
        onError: (error) => {
            setResult(null);
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
                onFileSelect={(file) => {
                    setResult(null);
                    setValue("manager.file", file, { shouldValidate: true })
                }}
            />
            {errors.manager?.file && (
                <p className="text-danger">{errors.manager.file.message}</p>
            )}

            <CsvFileSelect
                label="Your Lead List (Optional CSV)"
                onFileSelect={(file) => {
                    setResult(null);
                    setValue("isr.file", file, { shouldValidate: true })
                }}
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

            {result && (
                <div className="rounded-md border border-success bg-success/10 p-4 space-y-1">
                    <h3 className="font-semibold text-success">
                        Upload completed successfully
                    </h3>

                    <p>Total rows: <strong>{result.totalRows}</strong></p>
                    <p>Imported leads: <strong>{result.importedLeads}</strong></p>
                    <p>Failed leads: <strong>{result.failedLeads}</strong></p>
                </div>
            )}
        </form>
    );
}



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