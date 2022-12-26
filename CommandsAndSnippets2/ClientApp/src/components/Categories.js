import {useEffect, useState} from 'react';
import {getCategories} from "../recipeEndpoints";

const Categories = () => {
  const [categories, setCategories] = useState([]);
  useEffect(() => {
    getCategories()
      .then((categories) => {
        setCategories(categories);
      })
  }, [getCategories])

  const listCategories = () =>
    categories.map(x =>
        <div className="s12 m12 l4" key={x.id}>
          <div className="fill medium-height middle-align center-align">
            <div className="center-align">
              <i className="extra">video_library</i>
              <h5 style={{placeContent: "center"}}>{x.categoryName}</h5>
              <div className="space"></div>
              <nav className="center-align">
                <button className="round" disabled="disabled">View category</button>
              </nav>
            </div>
          </div>
      </div>);


  return (
    <div className="padding">
      <div className="grid">
        {listCategories()}
      </div>
    </div>
  )
}

export default Categories