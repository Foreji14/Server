const host = 'https://localhost:7176/api/'


const getAllCharacters = async () => {
    let res = {}
    await fetch(host + "Character/GetAllCharacters")
        .then(response => response.json())
        .then(response => {
            res = response.data;
        })
        .catch(err => console.error(err));
    return res
}

const getCharacterById = async (id) => {
    let res = {}
    await fetch(host + 'Character/' + id)
        .then(response => response.json())
        .then(response => res = response.data)
        .catch(err => console.error(err));

    return res;
}

const getAllCharacterClasses = async () => {
    let res = {}
    await fetch(host + 'Class/GetAllCharacterClasses')
        .then(response => response.json())
        .then(response => {
            res = response.data;
        })
        .catch(err => console.error(err))
    return res;
}
const getAllTraits = async () => {
    let res = {}
    await fetch(host + 'Trait/GetAllTraits')
        .then(response => response.json())
        .then(response => {
            res = response.data;
        })
        .catch(err => console.error(err))
    return res;
}
const deleteCharacterById = async (characterId) => {
    let res = {}
    await fetch(host + 'Character/' + characterId, {
        method: 'DELETE'
    })
        .then(response => response.json())
        .then(response => {
            res = response.data;
        })
        .catch(err => console.error(err))
    return res;
}

const updateCharacterById = async (id, updateCharacter) => {
    let res = {}
    await fetch(host + 'Character/UpdateCharacterById/' + id, {
        method: 'PUT',
        body: JSON.stringify(updateCharacter),
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(response => res = response.data)
        .catch(err => console.error(err))
    return res;
}


export { getCharacterById, updateCharacterById, deleteCharacterById, getAllCharacterClasses, getAllCharacters, getAllTraits };