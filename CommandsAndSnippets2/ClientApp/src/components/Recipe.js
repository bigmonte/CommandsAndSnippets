import React, {useEffect, useState} from "react";
import {useParams} from 'react-router-dom'
import {getRecipeById, getRecipeContents} from "../recipeEndpoints";
import {Prism as SyntaxHighlighter} from 'react-syntax-highlighter';
import {dark} from 'react-syntax-highlighter/dist/esm/styles/prism';

const Recipe = () => {
  const {id} = useParams()
  const [recipe, setRecipe] = useState(undefined);
  const [contents, setContents] = useState([]);
  const [loaded, setLoaded] = useState(false);

  useEffect(() => {
    getRecipeById(id).then(recipe => {
      setLoaded(true);
      if (recipe) {
        setRecipe(recipe);
        getRecipeContents(recipe.id).then((contents) => {
          const sortedContents = contents.sort((a, b) => parseFloat(a.order) - parseFloat(b.order));
          setContents(sortedContents);
        })
      }
    })
  }, [getRecipeById, getRecipeContents])

  /*public enum ContentType
 {
   Heading = 1,
     Description = 2,
     Note = 3,
     Html = 4,
     Snippet_JS = 10,
     Snippet_CS = 11,
     Snippet_Shell = 12,
     Snippet_Haskell = 13,
     Snippet_C = 14,
     Snippet_JSX = 15,
     Snippet_TypeScript = 16,
     Snippet_Conf = 17
 }*/
  const listContents = () =>
    contents.map(x => {
        if (x.contentType === 1) { // Heading
          return (<h5>{x.text}</h5>)
        }
        if (x.contentType === 2) { // Note
          return (<article>
            <details open="">
              <summary className="none">
                <div className="row">
                  <div className="max">

                  </div>
                </div>
              </summary>
              <p>{x.text}</p></details>
          </article>)
        }
        if (x.contentType === 10) { // Snippet_JS
          return (
            <SyntaxHighlighter language="js" style={dark}>
              {x.text}
            </SyntaxHighlighter>
          )
        }
        if (x.contentType === 11) { // Snippet_CS
          return (
            <SyntaxHighlighter language="cs" style={dark}>
              {x.text}
            </SyntaxHighlighter>
          )
        }
        if (x.contentType === 12) { // Snippet_Shell
          return (
            <SyntaxHighlighter language="sh" style={dark}>
              {x.text}
            </SyntaxHighlighter>
          )
        }
        if (x.contentType === 13) { // Snippet_Haskell
          return (
            <SyntaxHighlighter language="haskell" style={dark}>
              {x.text}
            </SyntaxHighlighter>
          )
        }
        if (x.contentType === 14) { // Snippet_C
          return (<SyntaxHighlighter language="c" style={dark}>
            {x.text}
          </SyntaxHighlighter>)
        }
        if (x.contentType === 15) { // Snippet_JSX
          return (<SyntaxHighlighter language="jsx" style={dark}>
            {x.text}
          </SyntaxHighlighter>)
        }
        if (x.contentType === 16) { // Snippet_TypeScript
          return (<SyntaxHighlighter language="typeScript" style={dark}>
            {x.text}
          </SyntaxHighlighter>)
        }
        if (x.contentType === 17) { // Snippet_Conf
          return (<SyntaxHighlighter language="conf" style={dark}>
            {x.text}
          </SyntaxHighlighter>)
        }
      }
    );


  return (
    loaded && recipe && (
      <>
      <div className="padding">
        <h4><span>{recipe.title}</span></h4>
      </div>
        <div className="small-divider"></div>
        <div className="padding">
          {listContents()}
        </div>
      </>
    )
  );
};

export default Recipe