import type React from "react";

export default function Button({
    children,
    type = "button",
    className = "btn btn-primary",
    ...rest
}: ButtonProps) {
    return (
        <button type={type ?? "button"} 
            className={className ?? "btn btn-primary"} 
            {...rest} >
            {children}
        </button>
    )
}

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
    children: React.ReactNode;
    onClick?(): void;
    className?: string;
    type?: "button" | "submit" | "reset";
    disabled?: boolean;
}