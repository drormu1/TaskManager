import React from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Typography,
  Grid,
  Chip,
  Box
} from '@mui/material';

interface TaskDetailsProps {
  open: boolean;
  onClose: () => void;
  task: any; // Replace 'any' with your ManagedTaskDto type if available
}

export const TaskDetails: React.FC<TaskDetailsProps> = ({ open, onClose, task }) => {
  if (!task) return null;

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>Task Details</DialogTitle>
      <DialogContent dividers>
        <Grid container spacing={2}>
          <Grid item xs={6}>
            <Typography variant="subtitle2" color="text.secondary">ID</Typography>
            <Typography variant="body1">{task.id}</Typography>
          </Grid>
          <Grid item xs={6}>
            <Typography variant="subtitle2" color="text.secondary">Title</Typography>
            <Typography variant="body1">{task.title}</Typography>
          </Grid>
          <Grid item xs={6}>
            <Typography variant="subtitle2" color="text.secondary">Type</Typography>
            <Typography variant="body1">{task.taskTypeName}</Typography>
          </Grid>
          <Grid item xs={6}>
            <Typography variant="subtitle2" color="text.secondary">Status</Typography>
            <Typography variant="body1">{task.taskStatusName}</Typography>
          </Grid>
          <Grid item xs={6}>
            <Typography variant="subtitle2" color="text.secondary">Assigned To</Typography>
            <Typography variant="body1">{task.assignedUserName}</Typography>
          </Grid>
          <Grid item xs={6}>
            <Typography variant="subtitle2" color="text.secondary">Created</Typography>
            <Typography variant="body1">{task.createdAt}</Typography>
          </Grid>
          <Grid item xs={12}>
            <Typography variant="subtitle2" color="text.secondary">State</Typography>
            <Box mt={1}>
              {task.isClosed ? (
                <Chip label="Closed" color="default" />
              ) : (
                <Chip label="Open" color="success" />
              )}
            </Box>
          </Grid>
        </Grid>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} variant="contained" color="primary">
          Close
        </Button>
      </DialogActions>
    </Dialog>
  );
};