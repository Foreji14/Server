import { Button, Card, CardContent, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {deleteCharacterById, getAllCharacters} from "../helpers/characters";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

const getCharacters = async () => await getAllCharacters();

function Characters() {
    let [characters, setCharacters] = useState();

    const handleDelete =  (id) =>{
        const res = async() => await deleteCharacterById(id);
        res();
        let newCharacters = characters.filter(x=>x.characterId !== id)
        setCharacters(newCharacters) 
    }

    useEffect(() => {
        try {
            const eff = async () => {
                const data = await getAllCharacters();
                setCharacters(data);
            }
            eff()
        } catch (err) {
            console.log(err)
        }
    }, [])

    return (
        <Box display={'flex'}>
            {characters !== undefined ?
                characters.map((character, idx) =>
                (<Card key={idx + 'chr'} sx={{backgroundColor:'#9C27B0', margin:1, width:'fit-content' }}>
                    <CardContent>
                        <Typography sx={{ fontSize: 14, color:'white' }} gutterBottom>
                            {character.characterClass.name}
                        </Typography>
                        <Typography variant="h5" component="div" sx={{color:'white'}}>
                            {character.name}
                        </Typography>
                        <Typography sx={{ mb: 1.5, color:'white' }}>
                            {character.traits.map((trait, x) => trait.name + ' ')}
                        </Typography>
                        <Typography variant="body2" sx={{color:'white'}}>
                            {character.description}
                        </Typography>
                        <Typography variant="body2"sx={{color:'white'}}>
                            {character.story}
                        </Typography>
                    </CardContent>
                    <Box display={'flex'}>
                    <Button variant="text" sx={{margin:2,alignSelf:'center',color:'white',backgroundColor:'#9C27B0'}} component={Link} to={'/characters/' + character.characterId} >Read More</Button>
                    <Button startIcon={<EditIcon/>} variant="text" sx={{margin:'auto',marginRight:'.4rem',alignSelf:'center',color:'white',backgroundColor:'#9C27B0'}} component={Link} to={'/characters/edit/' + character.characterId}/>
                    <Button onClick={() => handleDelete(character.characterId)} startIcon={<DeleteIcon/>} variant="text" sx={{margin:'auto',marginRight:'.4rem',alignSelf:'center',color:'white',backgroundColor:'#9C27B0'}}/>
                    </Box>
                </Card>)
                )
                : 'wait for characters...'}
                <Button variant="contained" sx={{margin: 'auto', marginLeft:1, backgroundColor:'#9C27B0'}} component={Link} to='/characters/add'>+</Button>
        </Box>
    )
}

export default Characters;