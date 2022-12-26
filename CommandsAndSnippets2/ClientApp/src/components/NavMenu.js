import { useState, useEffect } from 'react';
import { Button, ButtonGroup, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link, Navigate } from 'react-router-dom';
import './NavMenu.css';
import ytb from './cr_logo1.png';
import { LoggedInProfileMenu } from './LoggedInProfileMenu';
import * as ReactDOM from 'react-dom';
import {NonLoggedInProfileMenu} from "./NonLoggedInProfileMenu";

function NavMenu() {
  let [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  
  const lightMode = "\n" +
    "  --primary:#006a5f;\n" +
    "  --on-primary:#ffffff;\n" +
    "  --primary-container:#74f7e5;\n" +
    "  --on-primary-container:#00201c;\n" +
    "  --secondary:#4a635f;--on-secondary:\n" +
    "  #ffffff;--secondary-container:#cde8e2;\n" +
    "  --on-secondary-container:#05201c;\n" +
    "  --tertiary:#466179;\n" +
    "  --on-tertiary:#ffffff;\n" +
    "  --tertiary-container:#cbe5ff;\n" +
    "  --on-tertiary-container:#001d31\n" +
    "  ;--error:#ba1b1b;\n" +
    "  --error-container:#ffdad4;\n" +
    "  --on-error:#ffffff;\n" +
    "  --on-error-container:#410001;\n" +
    "  --background:#fafdfa;\n" +
    "  --on-background:#191c1b;\n" +
    "  --surface:#fafdfa;\n" +
    "  --on-surface:#191c1b;\n" +
    "  --surface-variant:#dbe5e2;\n" +
    "  --on-surface-variant:#3f4947;\n" +
    "  --outline:#6e7976;\n" +
    "  --inverse-on-surface:#eff1ef;\n" +
    "  --inverse-surface:#2d3130;\n" +
    "  --inverse-primary:#53dbc9;\n" +
    "  --shadow:#000000;\" \n" +
    "        class=\"light\"";

  const darkModeStyle = "\n" +
    "  --primary: #D0BCFF;\n" +
    "  --on-primary: #371E73;\n" +
    "  --primary-container: #4F378B;\n" +
    "  --on-primary-container: #EADDFF;\n" +
    "  --secondary: #CCC2DC;\n" +
    "  --on-secondary: #332D41;\n" +
    "  --secondary-container: #4A4458;\n" +
    "  --on-secondary-container: #E8DEF8;\n" +
    "  --tertiary: #EFB8C8;\n" +
    "  --on-tertiary: #492532;\n" +
    "  --tertiary-container: #633B48;\n" +
    "  --on-tertiary-container: #FFD8E4;\n" +
    "  --error: #F2B8B5;\n" +
    "  --on-error: #601410;\n" +
    "  --error-container: #8C1D18;\n" +
    "  --on-error-container: #F9DEDC;\n" +
    "  --background: #1C1B1F;\n" +
    "  --on-background: #E6E1E5;\n" +
    "  --surface: #1C1B1F;\n" +
    "  --on-surface: #E6E1E5;\n" +
    "  --outline: #938F99;\n" +
    "  --surface-variant: #49454F;\n" +
    "  --on-surface-variant: #CAC4D0;\n" +
    "  --inverse-surface: #E6E1E5;\n" +
    "  --inverse-on-surface: #313033;"

  const [collapsed, setCollapsed] = useState(false);
  const [darkMode, setDarkMode] = useState(
    window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches,
  );

  useEffect(() => {
    fetch('/auth/status')
      .then(response => response.json())
      .then(data => {
        setIsAuthenticated(data.isAuthenticated);
        setIsLoading(false);
        console.log(data)
        console.log('nav say', data.isAuthenticated)
      });
    const element = document.body;
    let node = ReactDOM.findDOMNode(element);
    if (darkMode === true) {
      node.style = darkModeStyle;
      if (node.classList.contains('light')) {
        node.classList.remove('light');
      }
      if (!node.classList.contains('dark')) {
        node.classList.add('dark');
      }
    } else {
      node.style = lightMode;
      if (node.classList.contains('dark')) {
        node.classList.remove('dark');
      }
      if (!node.classList.contains('light')) {
        node.classList.add('light');
      }
    }

  }, [darkMode]);

  const toggleNavbar = () => {
    setCollapsed(!collapsed);
  };

  const switchLights = () => {
    setDarkMode(!darkMode);
  };

  return (<>
      <nav className="top">
        <div className="space"/>
        <button className="circle large transparent m l small-margin" data-ui="#modal-expanded"><i>menu</i>
        </button>
        <Link to="/" className="logo-url">
          <img className="logo" src={ytb} alt="Code Recipes"/>
        </Link>
        <div className="max"/>
        <button className="transparent circle" onClick={switchLights}>
          <i>light_mode</i>
        </button>
          <button className="circle large transparent" onClick={toggleNavbar}>
            <i className="extra">account_circle</i>
            {collapsed && isAuthenticated &&  <LoggedInProfileMenu/>}
            {collapsed && !isAuthenticated &&  <NonLoggedInProfileMenu/>}
            
          </button>
      </nav>
      <div className="modal left small" id="modal-expanded">
        <header className="fixed">
          <nav>
            <button className="transparent circle" data-ui="#modal-expanded"><i>menu</i></button>
            <a>
              <img className="logo" src={ytb}/>
            </a>
          </nav>
        </header>

        <Link className="row round transparent" to="/" data-ui="#modal-expanded">
          <i>home</i>
          <div>Home</div>
        </Link>
        <Link className="row round transparent" to="/create-recipe" data-ui="#modal-expanded">
          <i>whatshot</i>
          <div>Create Recipe</div>
        </Link>

        <Link className="row round transparent" to="/categories" data-ui="#modal-expanded">
          <i>video_library</i>
          <div>Categories</div>
        </Link>
        <div className="small-divider"/>
        <a className="row round transparent" data-ui="#modal-expanded"><i>history</i>
          <div>Favorites</div>
        </a>
        <Link className="row round transparent" to="/my-recipes" data-ui="#modal-expanded">
          <i>slideshow</i>
          <div>My Recipes</div>
        </Link>
      </div>
    </>
  );
}

export {NavMenu as default};