import { useRef } from "react";
import "./App.css";
import useContentAssignmentClient from "./hooks/ContentAssignmentClientHook";
import ContentList from "./components/ContentList";
import PatientInput from "./components/PatientInput";

function App() {
	const client = useContentAssignmentClient();
	const contentIdInput = useRef<HTMLSelectElement>(null);
	const patientIdInput = useRef<HTMLInputElement>(null);

	const handleSubmit = () => {
		const patientId = patientIdInput.current?.value;
		const contentId = contentIdInput.current?.value;
		if (!contentId || !patientId)
			throw new Error("No content and/or patient selected");

		client.AssignContent(contentId, patientId);
	};

	return (
		<div className="card">
			<ContentList ref={contentIdInput} />
			<PatientInput ref={patientIdInput} />
			<button onClick={handleSubmit}>Assign</button>
		</div>
	);
}

export default App;
