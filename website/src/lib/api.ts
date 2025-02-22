import { API_TOKEN, AUTH_ROUTES, USER_ROUTES, BOARD_ROUTES, TASK_ROUTES, BOARD_USER_ROUTES } from './constants';

export const login = async (username: string, password: string) => {
    const response = await fetch(AUTH_ROUTES.LOGIN, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(    
            {
                "apiKey": API_TOKEN,
                "username": username,
                "passwordHash":  password
            }
        )
    });
    const data = response.json();
    return data;
}

export const persist = async (token: string) => {
    const response = await fetch(AUTH_ROUTES.REGISTER, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token
                }
            )
         });
    return response.json();
}

export const getUser = async (token: string) => {
    const response = await fetch(USER_ROUTES.GET, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token
                }
            )
         });
    return response.json();
}

export const createUser = async (email: string, username: string, password: string) => {
    const response = await fetch(USER_ROUTES.CREATE, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "email": email,
                    "username": username,
                    "passwordHash": password
                }
            )
         });
    return response.json();
}

export const getBoard = async (token: string, boardId: string) => {
    const response = await fetch(BOARD_ROUTES.GET, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "boardId": boardId
                }
            )
         });
    return response.json();
}

export const createBoard = async (token: string, name: string) => {
    const response = await fetch(BOARD_ROUTES.CREATE, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "name": name
                }
            )
         });
    return response.json();
}

export const changeOwner = async (token: string, boardId: string, newOwner: string) => {
    const response = await fetch(BOARD_ROUTES.CHANGE_OWNER, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "boardId": boardId,
                    "owner": newOwner
                }
            )
         });
    return response.json();
}

export const addStage = async (token: string, boardId: string, name: string, index: number) => {
    const response = await fetch(BOARD_ROUTES.ADD_STAGE, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "boardId": boardId,
                    "stageNumber": index,
                    "stageName": name
                }
            )
         });
    return response.json();
}

export const deleteStage = async (token: string, boardId: string, index: number) => {
    const response = await fetch(BOARD_ROUTES.DELETE_STAGE, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "boardId": boardId,
                    "stageNumber": index,
                    "stageName": ""
                }
            )
         });
    return response.json();
}

export const renameStage = async (token: string, boardId: string, index: number, name: string) => {
    const response = await fetch(BOARD_ROUTES.RENAME_STAGE, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "boardId": boardId,
                    "stageNumber": index,
                    "stageName": name
                    
                }
            )
         });
    return response.json();
}

export const createTask = async (token: string, boardId: string, stage: number, title: string, description: string, assignee: string | undefined) => {
    const response = await fetch(TASK_ROUTES.CREATE, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "title": title,
                    "description": description,
                    "fk_Board": boardId,
                    "fk_UserAssignee": assignee
                }
            )
         });
    return response.json();
}

export const updateTask = async (token: string, taskId: string, title: string, description: string, assignee: string | undefined, boardId: string) => {
    const response = await fetch(TASK_ROUTES.UPDATE, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "id": taskId,
                    "fk_Board": boardId,
                    "title": title,
                    "description": description,
                    "fk_UserAssignee": assignee
                }
            )
         });
    return response.json();
} 

export const changeVisibility = async (token: string, taskId: string, visibility: boolean, boardId: number) => {
    const response = await fetch(TASK_ROUTES.CHANGE_VISIBILITY, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "id": taskId,
                    "visibility": visibility,
                    'fk_Board': boardId
                }
            )
         });
    return response.json();
}

export const moveTask = async (token: string, taskId: string, stage: number, boardId: number) => {
    const response = await fetch(TASK_ROUTES.MOVE_TASK, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "id": taskId,
                    "stage": stage,
                    'fk_Board': boardId
                }
            )
         });
    return response.json();
}

export const changeAssignee = async (token: string, taskId: string, assignee: string, boardId: number) => {
    const response = await fetch(TASK_ROUTES.CHANGE_ASSIGNEE, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "id": taskId,
                    'fk_Board': boardId,
                    "assignee": assignee,

                }
            )
         });
    return response.json();
}

export const inviteUser = async (token: string, invite: string) => {
    const response = await fetch(BOARD_USER_ROUTES.INVITE, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "invite": invite
                }
            )
         });
    return response.json();
}

export const leaveBoard = async (token: string, boardId: string) => {
    const response = await fetch(BOARD_USER_ROUTES.LEAVE, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/jsn',
        },
        body: JSON.stringify(    
                {
                    "apiKey": API_TOKEN,
                    "token": token,
                    "boardId": boardId
                }
            )
         });
    return response.json();
}