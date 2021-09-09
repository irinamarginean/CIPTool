import * as React from 'react';
import { Button } from '../../../atoms/button/button';
import { Popover } from '../popover';

import { arrowPositions } from '../constants';

interface AttachedPopoverGalleryDemonstratorProps {
  paragraph: string;
}

const AttachedPopoverGalleryDemonstrator: React.FunctionComponent<AttachedPopoverGalleryDemonstratorProps> = ({
  paragraph,
}) => (
  <div className="frontend-kit-example_attached-popover-gallery">
    {arrowPositions.map(arrowPosition => {
      return (
        <div
          key={arrowPosition}
          style={{
            display: 'block',
            marginBottom: '1rem',
            textAlign: 'center',
          }}
        >
          <Button label={arrowPosition} action={arrowPosition} />
          <Popover
            paragraph={paragraph}
            buttonLabel="close me"
            closeButton
            detached={false}
            arrowPosition={arrowPosition}
          />
        </div>
      );
    })}
  </div>
);

export default AttachedPopoverGalleryDemonstrator;
