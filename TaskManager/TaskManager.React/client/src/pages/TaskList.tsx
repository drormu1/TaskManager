import React, { useEffect, useState } from 'react';
import { Box, Button, Chip, TextField, Typography } from '@mui/material';
import { Grid } from '@mui/material';
import { DataGrid, type GridColDef } from '@mui/x-data-grid';
import type { GridRenderCellParams } from '@mui/x-data-grid';
import { getTasksForUser } from '../api/taskService';
import type { ManagedTaskDto } from '../schemas/ManagedTaskDto';

import { ChangeStatus } from '../components/ChangeStatus';
import { TaskDetails } from '../components/TaskDetails';
import { getUsers } from '../api/userService';
import { type User } from '../interfaces/User';
import { getTaskTypes } from '../api/taskTypeService';
import { useNavigate } from 'react-router-dom';




export function TaskList() {

  const navigate = useNavigate();
  
  const [tasks, setTasks] = useState<ManagedTaskDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [userId, setUserId] = useState<string>('1');
  const [filterValue, setFilterValue] = useState<string>('1');
  
  const [detailsOpen, setDetailsOpen] = useState(false);
  const [changeStatusOpen, setChangeStatusOpen] = useState(false);
  const [selectedTask, setSelectedTask] = useState<ManagedTaskDto | null>(null);
  
  
  useEffect(() => {
    setLoading(true);
    getTasksForUser(Number(userId))
      .then(setTasks)
      .finally(() => setLoading(false));
  }, [userId]);

const handleDetails = (id: number) => {
  const task = tasks.find(t => t.id === id);
  setSelectedTask(task || null);
  setDetailsOpen(true);
};

const handleChangeStatus = (id: number) => {
  const task = tasks.find(t => t.id === id);
  setSelectedTask(task || null);
  setChangeStatusOpen(true);
};

const handleCreateClick = () => {
  setCreateOpen(true);
};

  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 70 },
    { field: 'title', headerName: 'Title', width: 150 },
    { field: 'taskTypeName', headerName: 'Type', width: 130 },
    { field: 'taskStatusName', headerName: 'Status', width: 150 },
    { field: 'assignedUserName', headerName: 'Assigned To', width: 130 }, // Use the correct property name
    { field: 'createdAt', headerName: 'Created', width: 180 },
    {
      field: 'isClosed',
      headerName: 'isClosed',
      width: 120,
     
    },
    {
      field: 'actions',
      headerName: 'Actions',
      width: 250,
      sortable: false,
      renderCell: (params: GridRenderCellParams) => (
        <Box>
          <Button
            variant="contained"
            color="info"
            size="small"
            style={{ marginRight: 8 }}
            onClick={() => handleDetails(params.row.id)}
          >
            Details
          </Button>
          {!params.row.isClosed && (
            <Button
              variant="contained"
              color="warning"
              size="small"
              onClick={() => handleChangeStatus(params.row.id)}
            >
              Change Status
            </Button>
          )}
        </Box>
      ),
    },
  ];

  return (
    
    <Box sx={{ p: 4 }}>
      <Typography variant="h4" gutterBottom>
        Tasks
      </Typography>
    

      <ChangeStatus
        open={changeStatusOpen}
        onClose={() => setChangeStatusOpen(false)}
        onChange={status => {
          // TODO: Call your API to change status, then refresh the list
          setChangeStatusOpen(false);
        }}
        currentStatus={selectedTask?.taskStatusName || ''}
      />

      <TaskDetails
        open={detailsOpen}
        onClose={() => setDetailsOpen(false)}
        task={selectedTask}
      />

      <Grid container spacing={2} alignItems="center" sx={{ mb: 2 }}>
        <Grid item>
          <TextField
            label="User ID"
            value={filterValue}
            onChange={e => {
              // Only allow numbers >= 1
              const val = e.target.value.replace(/[^0-9]/g, '');
              setFilterValue(val === '' ? '' : String(Math.max(1, Number(val))));
            }}
            size="small"
            type="number"
            inputProps={{ min: 1 }}
          />
        </Grid>
        <Grid item>
          <Button
            variant="contained"
            onClick={() => setUserId(filterValue)}
            sx={{ height: '40px' }}
            disabled={!filterValue || Number(filterValue) < 1}
          >
            Filter
          </Button>
        </Grid>
        <Grid item>
        <Button onClick={() => navigate('/tasks/create')}>Create New Task</Button>
        </Grid>
      </Grid>
      <Box sx={{ height: 600, width: '100%' }}>
        <DataGrid
          rows={tasks}
          columns={columns}
          initialState={{ pagination: { paginationModel: { pageSize: 5 } } }}
          pageSizeOptions={[5, 10]}
          loading={loading}
          getRowId={row => row.id}
          disableRowSelectionOnClick
          localeText={{
            noRowsLabel: 'No tasks found',
          }}
        />
      </Box>
    </Box>
  );
}
