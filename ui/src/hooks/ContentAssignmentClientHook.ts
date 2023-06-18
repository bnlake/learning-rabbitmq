import { useState } from "react";
import ContentAssignmentClient from "../services/ContentAssignmentClient";

const useContentAssignmentClient = () => {
	const api_url = import.meta.env.API_URL ?? import.meta.env.VITE_API_URL;
	const [client] = useState(new ContentAssignmentClient(api_url));

	return client;
};

export default useContentAssignmentClient;
