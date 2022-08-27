import { Button, Card, Checkbox, FormControl, FormControlLabel, FormGroup, Input, InputLabel, MenuItem, Select, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useReducer, useState } from "react";
import { getAllCharacterClasses, getAllTraits } from '../helpers/characters'


function NewCharacter() {
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


    const handleSubmit = (e) => {
        e.preventDefault();
        let data = formInput; 
        fetch('https://localhost:7176/api/Character',{
            method:"POST",
            body:JSON.stringify(data),
            headers:{
                "Content-Type":"application/json"
            }
        }).then(response => response.json())
        .then(response => console.log("Success: ", JSON.stringify(response)))
        .catch(err => console.err("Error:", err))
        window.location.pathname = '/characters'
    }

    const handleInput = (e) => {
        let name = e.target.name;
        let newValue = e.target.value;
        setFormInput({ [name]: newValue })
    }
    const handleCharacterClassChange = (e) => {
        setCharacterClass(e.target.value);
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
        setFormInput({traits:traits})
        console.log(traits);
    }

    useEffect(() => {
        try {
            const eff = async () => {
                const chClasses = await getAllCharacterClasses();
                const chTraits = await getAllTraits();
                setCharacterClasses(chClasses);
                setTraits(chTraits);
            }
            eff()
        } catch (err) {
            console.log(err)
        }
    }, [])

    return (
        characterClasses !== undefined && characterTraits !== undefined ? (
            <form onSubmit={handleSubmit}>

                <Box p={'auto'} display='flex' flexDirection={'column'} maxWidth='50%' height={'100%'} alignSelf={'center'} m='auto'>
                    <Typography variant="h4" m={'auto'} mb='0' mt='2rem'>Create New Character</Typography>
                    <Box display={'flex'} flexDirection='column' m={'auto '} mt='5rem' width={'50%'}>
                        <Box mt={'1rem'} mb={'1rem'} >
                            <FormControl fullWidth>
                                <InputLabel htmlFor="characterName">Name</InputLabel>
                                <Input required onChange={handleInput} name="name" type="text" id="characterName" />
                            </FormControl>
                        </Box>
                        <Box mt={'1rem'} mb={'1rem'} >
                            <FormControl fullWidth>
                                <InputLabel htmlFor="characterDescription">Description</InputLabel>
                                <Input required onChange={handleInput} name="description" multiline type="text" id="characterDescription" />
                            </FormControl>
                        </Box>
                        <Box mt={'1rem'} mb={'1rem'} >
                            <FormControl fullWidth>
                                <InputLabel htmlFor="characterStory">Story</InputLabel>
                                <Input required onChange={handleInput} name="story" multiline={true} size="medium" type="text" id="characterStory" />
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
                                    <FormControlLabel key={idx + 'trlbl'} control={<Checkbox key={idx + 'tr'} onChange={handleTraitsChange} value={x.traitId}/>} label={x.name}/>
                                ))}
                            </FormGroup>
                        </Box>
                        <Input type="hidden" name="traits" value={JSON.stringify(traits)} />
                        <Button type="submit" variant="contained">Confirm</Button>

                    </Box>
                </Box></form>) : ''
    )
}

export default NewCharacter;