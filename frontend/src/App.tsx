/*import { BrowserRouter } from 'react-router';*/
import Menu from './components/Menu';
import AppRoutes from './AppRoutes';



export default function App() {
  

    return (
        <>
            <Menu />
            <div className="container mb-4">
                <AppRoutes />
            </div>
        </>
    );
}

