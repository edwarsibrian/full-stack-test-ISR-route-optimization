import {type ChangeEvent } from 'react';
export default function CsvFileSelect(props: CsvFileSelectProps) {
    const handleOnChange = (e: ChangeEvent<HTMLInputElement>) => {
        if (!e.target.files || e.target.files.length === 0) return;

        const fileSelected = e.target.files[0];

    //validation for csv file
        if (!fileSelected.name.endsWith('.csv')) {
            alert('Please select a valid CSV file.');
            return;
        }
        props.onFileSelect(fileSelected);
    }
    return (
        <div className="form-group">
            <label>{props.label}</label>
            <div>
                <input
                    type="file"
                    accept=".csv"
                    className="form-control"
                    onChange={handleOnChange} />
            </div>
        </div>
    );
}

interface CsvFileSelectProps {
    label: string;
    onFileSelect: (file: File) => void;
}