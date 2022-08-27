import { Box, Paper, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import { getCharacterById } from "../helpers/characters";




function Character() {
    let [character, setCharacter] = useState(undefined)
    let { id } = useParams();

    useEffect(() => {
        let get = async () => {
            let res = await getCharacterById(id);
            setCharacter(res);
        };
        get();
    }, [])

    return (
        character !== undefined ? (
            <Paper sx={{ padding: '1rem' }}>
                <Box display={'flex'} flexDirection='column' sx={{ 'borderBottom': '2px solid gray' }}>
                    <Typography m={'auto'} variant="h2">Character Profile</Typography>
                </Box>
                <Box>
                    <Box m={'auto'} flexDirection='row' display='flex'>
                        <Typography variant="h6">Name:</Typography>
                            <Box m={'auto'} ml='.5rem'>
                            <Typography variant="h6" color={'secondary'}>{character.name}</Typography>
                            </Box>
                    </Box>
                    <Box m={'auto'} flexDirection='row' display='flex'>
                        <Typography variant="h6">Traits:</Typography>
                            <Box m={'auto'} ml='.5rem' mr={'0'} p={'.3rem'} border={'1px solid purple'} borderRadius={'2px'}>
                            <Typography color={'secondary'}>{character.traits.map(t=>(t.name + ' '))}</Typography>
                            </Box>
                    </Box>
                    <Box m={'auto'} flexDirection='row' display='flex'>
                        <Typography variant="h6">Class:</Typography>
                            <Box m={'auto'} ml='.5rem' p={'.3rem'} border={'1px solid purple'} borderRadius={'2px'}>
                                <Typography color={'secondary'}>{character.characterClass.name}</Typography>
                            </Box>
                    </Box>
                    <Box m={'auto'} flexDirection='row' display='flex'>
                        <Typography variant="h6">Description:</Typography>
                            <Box m={'auto'} ml='.5rem' p={'.3rem'} border={'1px solid purple'} borderRadius={'2px'}>
                                <Typography color={'secondary'}>{character.description}</Typography>
                            </Box>
                    </Box>
                    <Box m={'auto'} flexDirection='row' display='flex'>
                        <Typography variant="h6">Story:</Typography>
                            <Box m={'auto'} ml='.5rem' p={'.3rem'} border={'1px solid purple'} borderRadius={'2px'}>
                                <Typography color={'secondary'}>{character.story}</Typography>
                            </Box>
                    </Box>
                </Box>
            </Paper>) : ''
    )
}

export default Character