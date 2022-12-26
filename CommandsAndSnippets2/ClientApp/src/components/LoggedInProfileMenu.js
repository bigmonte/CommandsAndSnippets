import React from 'react';
import { Link } from "react-router-dom";

export const LoggedInProfileMenu = () => {
  return (
    <div className="dropdown left no-wrap active" id="dropdown-add" data-ui="#dropdown-add">
      <Link to="/my-recipes" className="row transparent">
        <div className="min"><i>code</i></div>
        <div className="min">My Recipes</div>
      </Link>
      <Link to="/logout" className="row transparent">
        <div className="min"><i>logout</i></div>
        <div className="min">Logout</div>
      </Link>
    </div>
  );
};