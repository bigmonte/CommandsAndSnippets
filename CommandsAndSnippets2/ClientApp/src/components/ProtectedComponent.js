import { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';

function ProtectedComponent({ Component }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);


  useEffect(() => {
    localStorage.setItem('redirectUrl', window.location.pathname);

    fetch('/auth/status')
      .then(response => response.json())
      .then(data => {
        setIsAuthenticated(data.isAuthenticated);
        setIsLoading(false);
      });
  }, []);

  if (isLoading) {
    return (
      <div className="loader large">
        {/* Show loading indicator here */}
      </div>
    );
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" />;
  }

  return <Component />;
}
export default ProtectedComponent;