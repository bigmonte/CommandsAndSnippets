import {Home} from "./components/Home";
import {SampleHighlighter} from "./components/SampleHighlighter";
import SignUp from "./components/SignUp";
import MyRecipes from "./components/MyRecipes";
import Login from "./components/Login";
import ProtectedComponent from "./components/ProtectedComponent";
import React from "react";
import Recipe from "./components/Recipe";
import CreateRecipe from "./components/CreateRecipe";
import Categories from "./components/Categories";

const AppRoutes = [
  {
    index: true,
    element: <Home/>
  },

  {
    path: '/my-recipes',
    element: <MyRecipes/>
  },
  {
    path: '/sign-up',
    element: <SignUp/>
  },
  {
    path: '/login',
    element: <Login/>
  },
  {
    path: '/create-recipe',
    element: <ProtectedComponent Component={CreateRecipe} />
  },
  {
    path: '/recipe/:id',
    element: <Recipe />
  },
  {
    path: '/recipe/edit/:id',
    element: <CreateRecipe />
  },
  {
    path: '/categories',
    element: <Categories/>
  },
];

export default AppRoutes;
