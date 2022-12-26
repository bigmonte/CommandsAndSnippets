import React, { useState, useEffect } from 'react';
import { Link, useParams } from "react-router-dom";
import { Button } from "reactstrap";

const MyRecipes = (props) => {
  const [recipes, setRecipes] = useState([]);

  useEffect(() => {
    const getRecipes = async () => {
      const response = await fetch('/snippets', {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        }
      });
      const result = await response.json();
      if (result && result.length) {
        const sortedList = result.sort((a, b) => a.title.localeCompare(b.title));
        setRecipes(sortedList);
      }
    };
    getRecipes();
  }, []);

  const listRecipes = () => {
    return recipes.map(x =>
      <article key={x.id}>
        <details>
          <summary className="none">
            <div className="row">
              <div className="max">
                <h5>{x.title}</h5></div>
              <i>arrow_drop_down</i>
            </div>
          </summary>
          <nav className="no-space">
            <Button tag={Link} to={`/recipe/edit/${x.id}`}>
              <button className="border no-round max small" no-direction=""><i>create</i></button>
            </Button>
            <Button tag={Link} to={`/recipe/${x.id}`}>
              <button className="border no-round max small" no-direction=""><i>remove_red_eye</i></button>
            </Button>
          </nav>
          <div>

          </div>
        </details>
      </article>
    );
  }
    return (
      <div className="padding">
        <div className="row">
          <div className="max">
            <h6>My Code Recipes</h6></div>
          <Button tag={Link} to="/create-recipe">
            <button className="large transparent m l">
              <span>New Recipe</span>
            </button>
          </Button>
        </div>
        <div className="small-space"/>
        <div className="divider"/>
        <div className="small-space"/>
        <div className="container">

        </div>
          {listRecipes()}
      </div>
    );
}

export default MyRecipes