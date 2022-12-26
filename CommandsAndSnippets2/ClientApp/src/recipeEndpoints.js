export const getRecipeById = async (id) => {
  return await fetch(`/snippets/${id}`, {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    }
  }).then(r => r.json());
}

export const postRecipe = async (recipeObject) => {
  return await fetch("/snippets", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(recipeObject),
  })
    .then((r) => {
      console.log(r);
      if (r.status !== 200) return false;
      return r.json();
    })
};

export const postRecipeContents = async (recipeId, contents) => {
  return await fetch(`/snippets/${recipeId}/contents`, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(contents),
  })
    .then((r) => {
      if (r.status !== 200) return false;
      return r.json();
    })
};

export const getCategories = async () => {
  return await fetch('/api/categories', {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    }
  }).then(r => r.json())
}

export const getRecipeContents = async (recipeId) => {
  return await fetch(`/snippets/${recipeId}/contents`, {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    }
  }).then(r => r.json())
}

export const postCategory = async (categoryToAdd) => {
  return await fetch('/api/categories', {
    method: 'POST',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(categoryToAdd),
  }).then(r => {
    if (r.status !== 200) return false;
    return r.json()
  });
}