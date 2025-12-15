import CsvFileSelect from "../../../components/CsvFileSelect";

export default function UploadLeads() {
    return (
        <>
            <h3>Upload Leads</h3>
            <form>
                <h4>Manager Leads CSV</h4>
                <CsvFileSelect label="Select Manager Leads CSV File" onFileSelect={file=>()} />
            </form>
            

        </>
    )
}