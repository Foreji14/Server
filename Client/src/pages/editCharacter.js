import { Button, Card, Checkbox, FormControl, FormControlLabel, FormGroup, Input, InputLabel, MenuItem, Select, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useReducer, useState } from "react";
import { useParams } from "react-router-dom";
import { getAllCharacterClasses, getAllTraits, getCharacterById, updateCharacterById } from '../helpers/characters'

function EditCharacter(){
    const {id} = useParams(); 
    const [formInput, setFormInput] = useReducer(
        (state, newState) => ({ ...state, ...newState }),
        {
            name: '',
            story: '',
            description: '',
            characterClassId: 1,
            traits: []
        })
    const [characterClass, setCharacterClass] = useState(1);
    const [traits, setSelectedTraits] = useState([])
    const [characterClasses, setCharacterClasses] = useState(undefined)
    const [characterTraits, setTraits] = useState(undefined)
    const [character, setCharacter] = useState(undefined)

    const handleSubmit = (e) => {
        e.preventDefault();
        let data = formInput;
        let put = async()=> await updateCharacterById(id, formInput)
        put();
        window.location.pathname = 'characters/' + id;
    }

    const handleInput = (e) => {
        let name = e.target.name;
        let newValue = e.target.value;
        setFormInput({ [name]: newValue })
        console.log(formInput)
    }
    const handleCharacterClassChange = (e) => {
        setCharacterClass(e.target.value);
        setFormInput({characterClassId: e.target.value})
    }
    const handleTraitsChange = (e) => {
        let oldTraits = traits;
        if (oldTraits.includes(parseInt(e.target.value), 0)) {
            let newTraits = [];
            oldTraits.map((x) => {
                if (parseInt(e.target.value) !== parseInt(x)) newTraits.push(parseInt(x));
            })
            setSelectedTraits(newTraits);
        }
        else {
            oldTraits.push(parseInt(e.target.value));
            setSelectedTraits(oldTraits);
        }
        setFormInput({traits:oldTraits})
        console.log(formInput);
    }

    useEffect(() => {
        try {
            const eff = async () => {
                const chClasses = await getAllCharacterClasses();
                const chTraits = await getAllTraits();
                const ch = await getCharacterById(id);
                setCharacter(ch)
                setCharacterClasses(chClasses);
                setTraits(chTraits);
                let mappedTraits = ch.traits.map(t=>t.traitId);
                setFormInput({
                    name:ch.name,
                    characterClassId: ch.characterClass.characterClassId,
                    story: ch.story,
                    description: ch.description,
                    traits: mappedTraits
                });
                setCharacterClass(ch.characterClass.characterClassId)
                setSelectedTraits(mappedTraits)
            }
            eff()
        } catch (err) {
            console.log(err)
        }
    }, [])

    return (
        characterClasses !== undefined && characterTraits !== undefined && character !== undefined ? (
            <form onSubmit={handleSubmit}>

                <Box p={'auto'} display='flex' flexDirection={'column'} maxWidth='50%' height={'100%'} alignSelf={'center'} m='auto'>
                    <Typography variant="h4" m={'auto'} mb='0' mt='2rem'>Edit Character</Typography>
                    <Box display={'flex'} flexDirection='column' m={'auto '} mt='5rem' width={'50%'}>
                        <Box mt={'1rem'} mb={'1rem'} >
                            <FormControl fullWidth>
                                <InputLabel htmlFor="characterName">Name</InputLabel>
                                <Input required onChange={handleInput} name="name" type="text" id="characterName" defaultValue={character.name} />
                            </FormControl>
                        </Box>
                        <Box mt={'1rem'} mb={'1rem'} >
                            <FormControl fullWidth>
                                <InputLabel htmlFor="characterDescription">Description</InputLabel>
                                <Input required onChange={handleInput} name="description" multiline type="text" id="characterDescription" defaultValue={character.description} />
                            </FormControl>
                        </Box>
                        <Box mt={'1rem'} mb={'1rem'} >
                            <FormControl fullWidth>
                                <InputLabel htmlFor="characterStory">Story</InputLabel>
                                <Input required onChange={handleInput} name="story" multiline={true} size="medium" type="text" id="characterStory" defaultValue={character.story}/>
                            </FormControl>
                        </Box>
                        <Box mt={'1rem'} mb={'1rem'} >
                            <FormControl fullWidth>
                                <InputLabel htmlFor="characterClass">Class</InputLabel>
                                <Select
                                    required
                                    name="characterClassId"
                                    id="characterClass"
                                    value={characterClass}
                                    label="Class"
                                    onChange={handleCharacterClassChange} >
                                    {characterClasses.map((x, idx) => (
                                        <MenuItem key={idx + 'cls'} value={x.characterClassId}>{x.name}</MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                        </Box>
                        <Box mt={'1rem'} mb={'1rem'} >
                            <Typography variant="h6">Traits:</Typography>
                            <FormGroup>
                                {characterTraits.map((x, idx) => (
                                    <FormControlLabel key={idx + 'trlbl'} checked={traits.includes(x.traitId)} control={<Checkbox key={idx + 'tr'} onChange={handleTraitsChange} value={x.traitId}/>} label={x.name}/>
                                ))}
                            </FormGroup>
                        </Box>
                        <Input type="hidden" name="traits" value={JSON.stringify(traits)} />
                        <Button type="submit" variant="contained">Confirm</Button>

                    </Box>
                </Box></form>) : ''
    )
}

export default EditCharacter