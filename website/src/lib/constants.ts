const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL;
export const API_TOKEN = process.env.NEXT_PUBLIC_API_TOKEN; 

export const AUTH_ROUTES = {
    LOGIN: `${API_BASE_URL}/auth/login`,
    REGISTER: `${API_BASE_URL}/auth/persist`,
    };

export const USER_ROUTES = {
    GET: `${API_BASE_URL}/user`,
    CREATE: `${API_BASE_URL}/user/create`,
};

export const BOARD_ROUTES = {
    GET: `${API_BASE_URL}/board`,
    CREATE: `${API_BASE_URL}/board/create`,
    CHANGE_OWNER: `${API_BASE_URL}/board/changeOwner`,
    ADD_STAGE: `${API_BASE_URL}/board/addStage`,
    DELETE_STAGE: `${API_BASE_URL}/board/deleteStage`,
    RENAME_STAGE: `${API_BASE_URL}/board/renameStage`,
};

export const TASK_ROUTES = {
    CREATE: `${API_BASE_URL}/task/create`,
    UPDATE: `${API_BASE_URL}/task/edit`,
    CHANGE_VISIBILITY: `${API_BASE_URL}/task/changeVisibility`,
    MOVE_TASK: `${API_BASE_URL}/task/changeStage`,
    CHANGE_ASSIGNEE: `${API_BASE_URL}/task/changeAssignee`,
};

export const BOARD_USER_ROUTES = {
    INVITE: `${API_BASE_URL}/boardUser/invite`,
    LEAVE: `${API_BASE_URL}/boardUser/leave`,
}
