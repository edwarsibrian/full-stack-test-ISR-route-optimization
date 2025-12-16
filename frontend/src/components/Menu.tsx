import { NavLink } from 'react-router-dom';

export default function Menu() {
    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container-fluid">
                <NavLink className="navbar-brand" to="/">ISR Route Optimization</NavLink>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/upload-leads">Upload Leads</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/home-address">Home Address</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/results">Results</NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
}