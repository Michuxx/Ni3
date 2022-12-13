import AuthWrapper from './LoginStart/AuthWrapper';
import {Route, Routes} from 'react-router-dom';
import { BrowserRouter } from 'react-router-dom';
import MainPage from './MainPage/MainPage';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<AuthWrapper />} />
        <Route path="/MainPage" element={<MainPage />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  );
}

  const NotFound = () => {
    return(
      <h1>NOT FOUND</h1>

    );

  };

export default App;
