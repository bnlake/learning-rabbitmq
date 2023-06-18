import { LegacyRef, forwardRef } from "react";

const PatientInput = forwardRef((_, ref: LegacyRef<HTMLInputElement>) => {
	return (
		<div style={{ display: "flex", flexDirection: "column" }}>
			<label>Patient Identifier</label>
			<input ref={ref} />
		</div>
	);
});

export default PatientInput;
