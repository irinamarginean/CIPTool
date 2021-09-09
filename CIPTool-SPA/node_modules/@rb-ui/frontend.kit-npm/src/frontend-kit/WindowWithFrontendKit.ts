import Box from './atoms/box';
import Dialog from './molecules/dialog';
import Lightbox from './molecules/lightbox';
import Notification from './atoms/notification';

/**
 * interface for a window object that has the frontend-kit global
 * component registry published
 * This can be used to indicate to TypeScript that these
 * properties actually exists and what there types are
 *
 * register any published component here!
 */
export default interface WindowWithFrontendKit extends Window {
  boschFrontendKit: {
    Box: typeof Box;
    Dialog: typeof Dialog;
    Lightbox: typeof Lightbox;
    Notification: typeof Notification;
  };
}
