import React, { useState } from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, Select, MenuItem } from '@mui/material';

export interface ChangeStatusDialogProps {
  open: boolean;
  onClose: () => void;
  onChange: (status: string) => void;
  currentStatus: string;
}

export const ChangeStatus: React.FC<ChangeStatusDialogProps> = ({ open, onClose, onChange, currentStatus }) => {
  const [status, setStatus] = useState(currentStatus);

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Change Task Status</DialogTitle>
      <DialogContent>
        <Select
          value={status}
          onChange={e => setStatus(e.target.value as string)}
          fullWidth
        >
          <MenuItem value="Created">Created</MenuItem>
          <MenuItem value="InProgress">In Progress</MenuItem>
          <MenuItem value="Completed">Completed</MenuItem>
          {/* Add more statuses as needed */}
        </Select>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
        <Button
          variant="contained"
          onClick={() => onChange(status)}
        >
          Change
        </Button>
      </DialogActions>
    </Dialog>
  );
};