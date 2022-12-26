import React, {Component} from 'react';
import {Link} from "react-router-dom";

export class NonLoggedInProfileMenu extends Component {
  render() {
    return (
      <div className="dropdown left no-wrap active" id="dropdown-add" data-ui="#dropdown-add">

        <Link to="/sign-up" className="row transparent">
          <div className="min"><i>person_add</i></div>
          <div className="min">Sign-Up</div>
        </Link>
        <Link to="/login" className="row transparent">
          <div className="min"><i>login</i></div>
          <div className="min">Login</div>
        </Link>

      </div>
    )
  }
}
