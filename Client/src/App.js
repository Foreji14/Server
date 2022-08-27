import { Route, Routes } from "react-router-dom";
import MenuBar from "./components/menubar";
import NewCharacter from "./pages/addCharacter";
import Character from "./pages/character";
import Characters from "./pages/characters";
import EditCharacter from "./pages/editCharacter";


function App() {
  return (
    <div className="App">
      <MenuBar/>
      <Routes>
        <Route path="/characters" element={<Characters />} />
        <Route path="/characters/add" element={<NewCharacter />} />
        <Route path="/characters/:id" element={<Character />} />
        <Route path="/characters/edit/:id" element={<EditCharacter />} />
      </Routes>
    </div>
  );
}

export default App;
