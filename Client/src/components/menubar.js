import { Tabs, Tab } from '@mui/material'
import { useState } from 'react';
import { Link } from 'react-router-dom';

function MenuBar() {
    const [value, setValue] = useState(0);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };
    return (
        <Tabs value={value} centered
        indicatorColor='none'
            onChange={handleChange}>
            <Tab label="Home" component={Link} to={"/"} />
            <Tab label="Characters" component={Link} to={"/characters"} />
        </Tabs>
    )
}

export default MenuBar;
