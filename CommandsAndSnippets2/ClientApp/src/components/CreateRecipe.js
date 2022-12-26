import { useState, useEffect } from "react";
import './CreateRecipe.css'
import {useParams} from "react-router-dom";
import {getCategories, getRecipeById, postCategory, postRecipe, postRecipeContents} from "../recipeEndpoints";

const CreateRecipe = () => {
  const { id } = useParams()
  const editMode = id !== undefined;
  const [recipe, setRecipe] = useState({});
  const [categoryName, setCategoryName] = useState("");
  const [categories, setCategories] = useState([]);
  const [selectedCategories, setSelectedCategories] = useState({});
  const [recipeTitle, setRecipeTitle] = useState("");
  const [contents, setContents] = useState([]);
  const [contentType, setContentType] = useState("0");
  const pageTitle = editMode ? 'Edit code recipe' : 'New code recipe'
  
  // Content inputs
  const [inputs, setInputs] = useState([]);
  const [inputValues, setInputValues] = useState([]);
  const [contentTypes, setContentTypes] = useState([]);

  useEffect( () => {
    if(editMode) {
      getRecipeById(id).then(recipe => {
        if (recipe) {
          setRecipe(recipe);
          setRecipeTitle(recipe.title)
        }
      })
    }
    fetchCategories();
  }, []);

  const insertCategory = async () => {
  
  }

  const fetchCategories = async () => {
    await getCategories()
      .then(result => {
        if (result && result.length) {
          const sortedList = result.sort((a, b) => a.categoryName.localeCompare(b.categoryName));
          setCategories(sortedList);

          // Initialize checkbox state
          let checkBoxState = {}
          for (let i = 0; i < sortedList.length; i++) {
            const category = sortedList[i];
            checkBoxState[category.id] = false;
          }
          setSelectedCategories({...checkBoxState})
        }
      })
  }

  const handleCategorySubmit = async () => {
    const categoryToAdd = {'categoryName': categoryName};
    await postCategory(categoryToAdd)
      .then(newCategory => {
        categories.push(newCategory);
        const sortedList = categories.sort((a, b) => a.categoryName.localeCompare(b.categoryName))
        setCategories(sortedList);
        setCategoryName('');
      });
  }

  const handleCategoriesCheckboxesStateChange = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    let newSelectedState = { ...selectedCategories };
    newSelectedState[name] = value;
    setSelectedCategories(newSelectedState);
  }

  const handleContentTypeChange = (event) => {
    setContentType(event.target.value);
    
  }
  const handleCategoryNameChange = (event) => {
    setCategoryName(event.target.value);
  }
  
  const handleRecipeTitleChange = (event) => {
    setRecipeTitle(event.target.value);
  }

  const getSelectedCategories = () => {
    const keys = Object.keys(selectedCategories);
    let result = [];
    for (let i = 0; i < keys.length; i++) {
      const key = keys[i];
      if (selectedCategories[key]) {
        const category = categories.filter((x) => x.id === key)[0];
        result.push(category);
      }
    }
    return result;
  };

  const handleInputChange = (event, index) => {
    const newValues = [...inputValues]; // create a copy of the inputValues array
    newValues[index] = event.target.value; // update the value at the specified index
    setInputValues(newValues); // update the inputValues state variable
  };

  const handleContentTypeSubmit = () => {
    const inputForm = (key) => 
    <div key={key} className="field textarea label border large fill">
      <textarea onChange={event => handleInputChange(event, key)} />
      <label className="active">{contentType}</label>
    </div>
    setInputs(prevInputs => prevInputs.concat(inputForm(prevInputs.length)));
    setInputValues(prevValues => prevValues.concat("")); // initialize new value to empty string
    setContentTypes(prevTypes => prevTypes.concat(contentType)); // add the current contentType to the contentTypes array
    
  }
  
  const extractContentType = (contentTypeText) => {
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
    
    if(contentTypeText === "heading") {
      return 1
    }
    if(contentTypeText === "description") {
      return 2
    }
    if(contentTypeText === "html") {
      return 3
    }
    if(contentTypeText === "snippet_js") {
      return 10
    }
    if(contentTypeText === "snippet_cs") {
      return 11
    }
    if(contentTypeText === "snippet_shell") {
      return 12
    }
    if(contentTypeText === "snippet_haskell") {
      return 13
    }
    
    
  }

  const handleRecipeSave = async () => {
    let recipeObject = {
      title: recipeTitle,
      categories: getSelectedCategories()
    }

    await postRecipe(recipeObject)
      .then((newRecipe) => {
        setRecipe(newRecipe);
        // Handle contents
        
        const hasContents = inputValues && inputValues.length && inputValues.length > 0;
        
        if(hasContents) {
          let contentsToAdd = []
          for (let index = 0; index < inputValues.length; index++) {
            const contentText = inputValues[index];
            const contentType = contentTypes[index];
            contentsToAdd.push({
              text: contentText,
              contentType: extractContentType(contentType),
              order: index
            })
          }
          postRecipeContents(newRecipe.id, contentsToAdd).then(data => {
            if(data.success) {
              window.ui(".toast.blue");
            } else {
              // Show error
              console.log("error adding contents")
            }
          })
        } else {
          window.ui(".toast.blue");
        }
      });
  }
  
  
  

  const listCategories = () =>
     categories.map(x =>
      <span className="category-item" key={x.id}>
          <label className="checkbox">
              <input type="checkbox" name={x.id} value={x.id} checked={selectedCategories[x.id]}
                     onChange={handleCategoriesCheckboxesStateChange}/>
              <span>{x.categoryName}</span>
          </label>
      </span>);


  return (
      <>
        <div className="recipe-container">
          <div className="padding">
            <h5>{pageTitle}</h5>
          </div>
          <div className="padding grid">
            <div className="s8">
              <div className="grid">
                <div className="s12">
                  <div className="field label border">
                    <input type="text" name="recipeTitle" value={recipeTitle}
                           onChange={handleRecipeTitleChange}/>
                    <label className="active">Title</label>
                  </div>
                  {
                    inputs.length === 0 &&
                    <article className="border medium no-padding">
                      <div className="padding primary absolute center middle" data-ui="#modal"
                           style={{cursor: 'pointer'}}>
                        <h6>No Content. Click to add content.</h6>
                      </div>
                    </article>
                  }
                  {
                    inputs.length > 0 &&
                    inputs.map(input => input)
                  }
                </div>
              </div>
            </div>
            <div className="s6 m4">
              <button className="small" data-ui="#modal">
                <i>add</i>
                <span>Add Content</span>
              </button>
              <article className="small scroll">
                <header className="fixed">
                  <h5 className="no-margin">Categories</h5>
                </header>
                <div className="wrap">
                  {listCategories()}
                </div>
              </article>
              <div className="add-category small-margin" style={{paddingTop: '25px'}}>
                <h6>Add Category</h6>
                <div className="field border">
                  <input name="categoryName" type="text" value={categoryName}
                         onChange={handleCategoryNameChange}/>
                  <button className="small-text small-space small-padding" onClick={handleCategorySubmit}
                          style={{marginLeft: '0px', marginTop: '10px'}}>Add
                  </button>
                </div>
              </div>
            </div>
          </div>
          <nav className="left-align padding">
            <button data-ui="#modal-max" onClick={handleRecipeSave}>Save</button>
          </nav>
        </div>
        <div className="modal medium" id="modal">
          <div className="modal-title medium-padding">
            <h5>Add component</h5>
          </div>
          <div className="medium-padding">
            <div className="field label suffix border">
              <select name="contentType" value={contentType} onChange={handleContentTypeChange}>
                <option disabled value="0">Select a option...</option>
                <option value="heading">Heading</option>
                <option value="description">Description</option>
                <option value="note">Note</option>
                <option value="html">Html</option>
                <option value="snippet_js">JavaScript code</option>
                <option value="snippet_cs">C# Code</option>
                <option value="snippet_shell">Snippet Shell</option>
                <option value="snippet_haskell">Snippet Haskell</option>
                
              </select>
              <label className="active">Component Type</label>
              <i>arrow_drop_down</i>
            </div>
            <nav className="right-align">
              <button className="border" data-ui="#modal">Cancel</button>
              <button data-ui="#modal" onClick={handleContentTypeSubmit}>Confirm</button>
            </nav>
          </div>
        </div>
        <div className="toast blue white-text"><i>info</i><span>Recipe was successfully saved.</span></div>
      </>
    )
}

export {CreateRecipe as default};