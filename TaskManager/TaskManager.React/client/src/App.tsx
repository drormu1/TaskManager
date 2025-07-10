import { Routes, Route } from 'react-router-dom';
import { TaskList } from './pages/TaskList';
import { CreateTask } from './components/CreateTask';
import { useNavigate } from 'react-router-dom';

export function App() {
  // You may need to fetch users/taskTypes here and pass as props
  // Or use context/global state if you prefer
  const navigate = useNavigate();

  return (
    <Routes>
      <Route path="/" element={<TaskList />} />
      <Route path="/tasks/create" element={
        <CreateTask         
          onCreate={task => {
            navigate('/');
          }}
          onCancel={() => {
            navigate('/');
          }}
        />
      } />
    </Routes>
  );
}
export default App;