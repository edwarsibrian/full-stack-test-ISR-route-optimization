
export default function Button(props: ButtonProps) {
    return (
        <button type={props.type ?? "button"} 
            className={props.className ?? "btn btn-primary"} 
            onClick={props.onClick} 
            disabled={props.disabled ?? false}>
            {props.children}
        </button>
    )
}

interface ButtonProps {
    children: React.ReactNode;
    onClick?(): void;
    className?: string;
    type?: "button" | "submit" | "reset";
    disabled?: boolean;
}