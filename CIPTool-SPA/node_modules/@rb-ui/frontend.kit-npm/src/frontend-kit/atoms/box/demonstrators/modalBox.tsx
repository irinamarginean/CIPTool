import * as React from 'react';
import { Button } from '../../button/button';
import { Box } from '../box';

interface ModalBoxProps {
  modalId: string;
}

const ModalBox: React.FunctionComponent<ModalBoxProps> = ({ modalId }) => (
  <div
    className="frontend-kit-example_modal-box"
    data-frok-show-modal={modalId}
  >
    <Button label="click here" action="show" />

    <Box width="50vw" height="50vh" modal modalId={modalId}>
      <div
        style={{
          width: '100%',
          height: '100%',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
        }}
      >
        click anywhere to close
      </div>
    </Box>
  </div>
);

export default ModalBox;
