import {
  BrowserRouter as Router,
  Routes,
  Route
} from "react-router-dom";

import { HomePage } from '../page/HomePage';

export default function Navigation() {


  return (
      <Router>
      <Routes>
        <Route
          path="/"
          element={
              <HomePage />
          }
        />
      </Routes>
    </Router> 
  );
}